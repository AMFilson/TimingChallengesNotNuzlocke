// Filename: StandardRule.cs
// Author: Andrew Filson Jr
// Date: 2026-11-01
// Updated: 2026-11-23
// DESC: This class defines the rules for a timed challenge in a game. When a specific action is performed,
//        it triggers a consequence that lasts for a defined set of time.

using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using System.Text;


namespace TimingChallengesNotNuzlocke.TimingChallengeClasses
{
    internal class StandardRule : Rule
    {
       // All instance variables have now been inherited
        

        // parameterized constructor to initialize the Rules class with a time limit, action and consequence

        public StandardRule(int timeLimit, string action, string consequence) : base(timeLimit, action, consequence)
        {
        }


    }
}
