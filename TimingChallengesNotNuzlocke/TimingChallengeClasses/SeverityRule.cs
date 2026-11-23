// Filename: SeverityRule.cs
// Author: Andrew Filson Jr
// Date: 2026-11-23
// DESC: This class defines rules with a severity level which indicates how easy it is to activate
// and the severity of any consequences which result from a consequence triggering action

using System;
using System.Collections.Generic;
using System.Text;

namespace TimingChallengesNotNuzlocke.TimingChallengeClasses
{
    internal class SeverityRule : Rule
    {
        // just a simple index value containing the severity level
        private int severitylevel;
        private int actionRating;
        private int consequenceRating;

        // parameterized constructor for SeverityRule
        public SeverityRule(int timeLimit, string action, string consequence, int actionRating, int consequenceRating) : base( timeLimit, action, consequence)
        {
            if (actionRating < 1 || actionRating > 10)
            {
                throw new InvalidRuleException("Action ease rating must be between 1 and 10.");
            }

            if (consequenceRating < 1 || consequenceRating > 10)
            {
                throw new InvalidRuleException("Consequence impact rating must be between 1 and 10.");
            }

            // idk whats even wrong at this point bro why wont this exist in current context
            ActionRating = actionRating;
            ConsequenceRating = consequenceRating;
            

            // a calculation to derive the severity level based off the action level and consequence level
            // then just round it to an int value
            double average = (actionRating + consequenceRating) / 2.0;
            SeverityLevel = (int)Math.Round(average, MidpointRounding.AwayFromZero);
        }

        //overriden toString method for severity level IF IT WORKED
        public override string ToString()
        {
            return $"[Severity {SeverityLevel}/10] {Action}, {Consequence}, duration: {FormattedTimeLimit}";
        }

    }
}
