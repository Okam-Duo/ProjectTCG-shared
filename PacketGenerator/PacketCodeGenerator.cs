using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace PacketGenerator
{
    public static class PacketCodeGenerator
    {
        static string genPacket = "";
        static string packetEnums = "";
        static string packetIdPair = "";
        static string packetHandle = "";
        static string packetHandleSwitch = "";
        static int packetId = 1;

        public static void ParsePDL()
        {
            ResetStaticField();

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };

            using (XmlReader r = XmlReader.Create("../../../PacketGenerator/PDL.xml", settings))
            {
                r.MoveToContent();

                while (r.Read())
                {
                    if (r.Depth == 1 && r.NodeType == XmlNodeType.Element)
                    {
                        ParsePacket(r);
                    }
                }

                string packetFactoryText = string.Format(PacketFormat.packetFactoryFormat, packetIdPair);
                string packetHandlerText = string.Format(PacketFormat.packetHandlerFormat, packetHandleSwitch, packetHandle);

                string fileText = string.Format(PacketFormat.fileFormat, packetEnums, packetFactoryText, genPacket, packetHandlerText);

                File.WriteAllText("../../../Shared/Packets/GeneratedPacketCode.cs", fileText);
                Console.WriteLine("Success");
            }
        }

        private static void ResetStaticField()
        {
            genPacket = "";
            packetEnums = "";
            packetIdPair = "";
            packetHandle = "";
            packetHandleSwitch = "";
            packetId = 1;
        }

        private static void ParsePacket(XmlReader r)
        {
            if (r.NodeType == XmlNodeType.EndElement) return;
            if (r.Name.ToLower() != "packet")
            {
                Console.WriteLine("Invalid packet node");
                return;
            }

            string packetName = r["name"];
            if (string.IsNullOrEmpty(packetName))
            {
                Console.WriteLine("Packet without name");
                return;
            }

            Tuple<string, string, string, string, string> t;
            if (r.IsEmptyElement)
            {
                t = new("", "", "", "", "");
            }
            else
            {
                t = ParseMembers(r);
            }

            string initLogic = string.Format(PacketFormat.packetInitializerFormat, packetName, t.Item4, t.Item5);
            if (t.Item4.Length == 0) initLogic = "";

            genPacket += string.Format(PacketFormat.packetFormat, packetName, t.Item1, t.Item2, t.Item3, packetId, initLogic);
            packetEnums += $"\n\t\t{packetName} = {packetId++},";
            packetIdPair += $"\n\t\t{{PacketID.{packetName},Read<{packetName}>}},";
            packetHandle += string.Format(PacketFormat.packetHandleFunctionFormat, packetName);
            packetHandleSwitch += string.Format(PacketFormat.packetHandleSwitchFormat, packetName);


            Console.WriteLine(packetName);
        }

        private static Tuple<string, string, string, string, string> ParseMembers(XmlReader r)
        {
            string packetName = r["name"];

            string memberCode = "";
            string readCode = "";
            string writeCode = "";
            string initValues = "";
            string initLogic = "";

            int fieldCount = 0;

            int depth = r.Depth + 1;
            while (r.Read())
            {
                if (r.Depth != depth)
                {
                    break;
                }

                string memberName = r["name"];
                if (string.IsNullOrEmpty(memberName))
                {
                    Console.WriteLine("Member without name");
                    return null;
                }
                fieldCount++;

                if (string.IsNullOrEmpty(memberCode) == false)
                {
                    memberCode += Environment.NewLine;
                }
                if (string.IsNullOrEmpty(readCode) == false)
                {
                    readCode += Environment.NewLine;
                }
                if (string.IsNullOrEmpty(writeCode) == false)
                {
                    writeCode += Environment.NewLine;
                }
                if (string.IsNullOrEmpty(initLogic) == false)
                {
                    initLogic += Environment.NewLine;
                }

                Console.WriteLine($"    {memberName}");

                string memberType = r.Name;
                switch (memberType)
                {
                    case "bool":
                    case "short":
                    case "ushort":
                    case "int":
                    case "long":
                    case "float":
                    case "double":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readFormat, memberName, ToMemberType(memberType), memberType);
                        writeCode += string.Format(PacketFormat.writeFormat, memberName, memberType);
                        break;
                    case "string":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readStringFormat, memberName);
                        writeCode += string.Format(PacketFormat.writeStringFormat, memberName);
                        break;
                    case "list":
                        Tuple<string, string, string, string> t = ParseArray(r);
                        memberCode += t.Item1;
                        readCode += t.Item2;
                        writeCode += t.Item3;

                        memberType = t.Item4 + "[]";
                        break;
                    case "byte":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readByteFormat, memberName);
                        writeCode += string.Format(PacketFormat.writeByteFormat, memberName);
                        break;
                    case "enum":
                        Tuple<string, string, string, string> e = ParseEnum(r);
                        memberCode += e.Item1;
                        readCode += e.Item2;
                        writeCode += e.Item3;

                        memberType = e.Item4;
                        break;
                    default://구조체일 경우
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readStructFormat, memberName);
                        writeCode += string.Format(PacketFormat.writeStructFormat, memberName);
                        break;
                }

                readCode += "\n";
                writeCode += "\n";

                if (fieldCount == 1)
                {
                    initValues += $"{memberType} {memberName}";
                }
                else
                {
                    initValues += $", {memberType} {memberName}";
                }

                initLogic += $"this.{memberName} = {memberName};";
            }

            memberCode = memberCode.Replace("\n", "\n\t\t");
            readCode = readCode.Replace("\n", "\n\t\t\t");
            writeCode = writeCode.Replace("\n", "\n\t\t\t");
            initLogic = initLogic.Replace("\n", "\n\t\t\t");


            return new Tuple<string, string, string, string, string>(memberCode, readCode, writeCode, initValues, initLogic);
        }

        private static Tuple<string, string, string, string> ParseArray(XmlReader r)
        {
            string typeName = r["name"];

            if (string.IsNullOrEmpty(typeName))
            {
                Console.WriteLine("List without name");
                return null;
            }

            string arrayName = r["name"];


            int depth = r.Depth + 1;
            r.Read();

            string arrayType = r.Name;

            string readLogic;
            string writeLogic;
            string memberCode;

            if (arrayType == "enum")
            {
                arrayType = r["name"];

                memberCode = string.Format(PacketFormat.memberArrayFormat, arrayType, arrayName);

                readLogic = string.Format(PacketFormat.readEnumFormat, arrayName + "[i]", arrayType);
                writeLogic = string.Format(PacketFormat.writeEnumFormat, arrayName + "[i]", arrayType);
            }
            else
            {
                memberCode = string.Format(PacketFormat.memberArrayFormat, arrayType, arrayName);
                switch (arrayType)
                {
                    case "bool":
                    case "short":
                    case "ushort":
                    case "int":
                    case "long":
                    case "float":
                    case "double":
                        readLogic = string.Format(PacketFormat.readFormat, arrayName + "[i]", ToMemberType(arrayType), arrayType);
                        writeLogic = string.Format(PacketFormat.writeFormat, arrayName + "[i]", arrayType);
                        break;
                    case "string":
                        readLogic = string.Format(PacketFormat.readStringFormat, arrayName + "[i]");
                        writeLogic = string.Format(PacketFormat.readStringFormat, arrayName + "[i]");
                        break;
                    case "byte":
                        readLogic = string.Format(PacketFormat.readByteFormat, arrayName + "[i]");
                        writeLogic = string.Format(PacketFormat.readByteFormat, arrayName + "[i]");
                        break;
                    default://구조체일 경우
                        readLogic = string.Format(PacketFormat.readStructFormat, arrayName + "[i]");
                        writeLogic = string.Format(PacketFormat.writeStructFormat, arrayName + "[i]");
                        break;
                }
            }


            string readCode = string.Format(PacketFormat.readArrayFormat, readLogic);
            string writeCode = string.Format(PacketFormat.writeArrayFormat, arrayName, arrayType, writeLogic);


            r.Read(); //</list> 읽기

            return new Tuple<string, string, string, string>(memberCode, readCode, writeCode, arrayType);
        }

        private static Tuple<string, string, string, string> ParseEnum(XmlReader r)
        {
            string enumName = r["name"];

            if (string.IsNullOrEmpty(enumName))
            {
                Console.WriteLine("Enum without name");
                return null;
            }

            int depth = r.Depth + 1;
            r.Read();

            string enumType = r.Name;

            string memberCode = string.Format(PacketFormat.memberFormat, enumType, enumName);
            string readCode = string.Format(PacketFormat.readEnumFormat, enumName, enumType);
            string writeCode = string.Format(PacketFormat.writeEnumFormat, enumName, enumType);


            r.Read(); //</enum> 읽기

            return new Tuple<string, string, string, string>(memberCode, readCode, writeCode, enumType);
        }

        private static string ToMemberType(string memberType)
        {
            switch (memberType)
            {
                case "bool":
                    return "ToBoolean";
                case "short":
                    return "ToInt16";
                case "ushort":
                    return "ToUInt16";
                case "int":
                    return "ToInt32";
                case "long":
                    return "ToInt64";
                case "float":
                    return "ToSingle";
                case "double":
                    return "ToDouble";
                default:
                    return "";
            }
        }
    }
}

