using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contents
{
    public struct SkillTargetFilter
    {
        public int choiceCount;
        public TargetingType targetingType;
        public TargetTeam team;

        //어떤 팀의 대상을 타겟으로 정할 수 있는지
        public enum TargetTeam
        {
            None,
            Ally,
            Enemy,
            All,
        }

        //영웅 선택인지, 특정 팀의 영웅 전체 선택인지
        public enum TargetingType
        {
            None,
            Hero,
            Team,
        }
    }
}