﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace ShyRiven
{
    internal class Target
    {
        private static Obj_AI_Hero s_Target;
        private static bool s_Flashed;

        public static Obj_AI_Hero Get(float inRange, bool locked = false, TargetSelector.DamageType dtype = TargetSelector.DamageType.Physical)
        {
            if (s_Target != null)
            {
                if (!s_Target.IsValidTarget() || s_Target.IsDead)
                    s_Target = null;
                else if (s_Target.Distance(ObjectManager.Player.ServerPosition) > inRange)
                {
                    if (locked)
                        return null;
                    else
                        s_Target = null;
                }
            }

            if (s_Target == null)
                Set(TargetSelector.GetTarget(inRange, dtype));

            if (TargetSelector.SelectedTarget != null && TargetSelector.SelectedTarget != s_Target && TargetSelector.SelectedTarget.Distance(ObjectManager.Player.ServerPosition) < 1000)
                Set(TargetSelector.SelectedTarget);

            return s_Target;
        }

        public static void Set(Obj_AI_Hero t)
        {
            if (s_Target != t)
                s_Flashed = false;

            s_Target = t;
        }

        public static void SetFlashed(bool val = true)
        {
            s_Flashed = val;
        }

        public static bool IsTargetFlashed()
        {
            if (s_Target == null)
                throw new NullReferenceException("Target is null");

            return s_Flashed;
        }
    }
}
