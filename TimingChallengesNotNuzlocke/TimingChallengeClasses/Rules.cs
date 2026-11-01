using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using System.Text;

namespace TimingChallengesNotNuzlocke.TimingChallengeClasses
{
    internal class Rules
    {
        // Properties of the Rules class
        // Time limit defines the max time allowed for the challenge in secons
        private int timeLimit;
        // String assignments serve as the most appropriate data type for the action and consequence properties
        // as they are both textual descriptions of the challenge requirements and outcomes.
        // Action defines a specific action performed by the player that triggers a consequence
        private string action;
        // a consequence defines the penalty or outcome that occurs if the player fails to perform the action within the time limit
        private string consequence;

        // default constructor for Rules so it can be instantiated without parameters
        public Rules() { }
        

        // Constructor to initialize the Rules class with a time limit
        public Rules(int timeLimit, string action, string consequence)
        {
            TimeLimit = timeLimit;
            Action = action;
            Consequence = consequence;

        }

        // Now I need to create getters and setters for all defined properties
        public int TimeLimit { get; set; }

        public string Action { get; set; }
        public string Consequence { get; set; }

        // Now I need to override the ToString() method to provide a string representation of the Rules object
        public override string ToString()
        {
            return $"Rule: {Action}, {Consequence}, this rule is active for {TimeLimit} seconds";

        }

    }
}
