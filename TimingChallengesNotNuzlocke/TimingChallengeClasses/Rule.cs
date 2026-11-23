// Filename: Rule.cs
// Author: Andrew Filson Jr
// Date: 2026-11-23
// DESC: An abstract parent class for all rules

using System;
using System.Collections.Generic;
using System.Text;

namespace TimingChallengesNotNuzlocke.TimingChallengeClasses
{
    public abstract class Rule
    {

        // Properties of the Rules class
        // Time limit defines the max time allowed for the challenge in secons
        protected int timeLimit = 0;
        //Initial time limit for tracking and replacing rules
        protected int initialTimeLimit = 0;
        // String assignments serve as the most appropriate data type for the action and consequence properties
        // as they are both textual descriptions of the challenge requirements and outcomes.
        // Action defines a specific action performed by the player that triggers a consequence
        protected string action = "";
        // a consequence defines the penalty or outcome that occurs if the player fails to perform the action within the time limit
        protected string consequence = "";
        // A counter to track the amount of rules
        protected static int ruleCount = 0;

        protected static int count;


        // default constructor for Rules so it can be instantiated without parameters
        protected Rule() { }


        // parameterized constructor to initialize the Rules class with a time limit, action and consequence

        protected Rule(int timeLimit, string action, string consequence)
        {
            TimeLimit = timeLimit;
            InitialTimeLimit = timeLimit;
            Action = action;
            Consequence = consequence;
            ruleCount++;

        }

        // Now I need to create getters and setters for all defined properties
        public int TimeLimit { get; set; }

        public string Action { get; set; }
        public string Consequence { get; set; }

        // Adding a new property to track Initial Time Limit so I can replace the corresponding rule with another one
        public int InitialTimeLimit { get; set; }

        //just tracks total number of rules created so far
        protected static int Count { get

            { return ruleCount; }
        }

        // I need to format the displayed time so that its more readable for the user and not just seconds
        public string FormattedTimeLimit
        {
            // Need to use an explicit getter definition to convert the time limit from seconds to a readable format
            get
            {
                // TimeSpan is class which I can produce objects from
                // It's unique in that it represents an interval of time
                // DateTime and DateTimeOffest can be used to represent calendar dates
                TimeSpan challangeTimeLimit = TimeSpan.FromSeconds(TimeLimit);
                return challangeTimeLimit.ToString(@"mm\:ss");
            }
        }

        // Now I need to override the ToString() method to provide a string representation of the Rules object
        public override string ToString()
        {
            // A simplified string formatting of all rules for how they will be displayed in the ListBox of the MainWindow.xaml
            // This will be displayed via the ListBox.ItemTemplate binding to the Rules object in the MainWindow.xaml.cs file
            // Each rule object is placed in an ObservableCollection which is bound to the Listbox via the name of the ObservableCollection object
            return $"{Action}, {Consequence}, duration: {FormattedTimeLimit}";

        } 

    }
}
