// Filename: SeverityRule.cs
// Author: Andrew Filson Jr
// Date: 2026-11-23
// DESC: This class defines rules with a severity level which indicates how easy it is to activate
// and the severity of any consequences which result from a consequence triggering action

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace TimingChallengesNotNuzlocke.TimingChallengeClasses
{
    internal class SeverityRule : Rule
    {
        // just a simple index value containing the severity level
        private int severityLevel;
        private int actionRating;
        private int consequenceRating;

        // parameterized constructor for SeverityRule
        public SeverityRule(int timeLimit, string action, string consequence, int actionRating, int consequenceRating, int severityLevel) : base( timeLimit, action, consequence)
        {
            //Exception handling to throw for the new class, need to catch it in mainwindow.xaml.cs still
            if (actionRating < 1 || actionRating > 10)
            {
                throw new InvalidRuleException("Action rating must be between 1 and 10.");
            }

            if (consequenceRating < 1 || consequenceRating > 10)
            {
                throw new InvalidRuleException("Consequence rating must be between 1 and 10.");
            }

            // idk whats even wrong at this point bro why wont this exist in current context
            ActionRating = actionRating;
            ConsequenceRating = consequenceRating;
            
            

            // a calculation to derive the severity level based off the action level and consequence level
            // then just round it to an int value
            double average = (actionRating + consequenceRating) / 2.0;
            severityLevel = (int)Math.Round(average, MidpointRounding.AwayFromZero);
            SeverityLevel = severityLevel;
        }

        //getters and setters for all the new properties
        public int ActionRating { get; set; }
        public int ConsequenceRating { get; set; }

        public int SeverityLevel { get; set; }

        //overriden toString method for severity level IF IT WORKED
        public override string ToString()
        {
            return $"[Severity {SeverityLevel}/10] {Action}, {Consequence}, duration: {FormattedTimeLimit}";
        }

    }
}
