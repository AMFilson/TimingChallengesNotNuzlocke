// Filename: MainWindow.xaml.cs
// Author: Andrew Filson Jr
// Date: 2026-11-01
// DESC: This class contains the main logic of this TimingChallengesNotNuzlocke project. And data for Actions and Consequences.

using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

// Need to add this reference else it cant find my Rules.cs class
using TimingChallengesNotNuzlocke.TimingChallengeClasses;

namespace TimingChallengesNotNuzlocke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // I need a timer to keep track of the time for all rules.
        private DispatcherTimer ruleCountdownTimer = new DispatcherTimer();

        // A collection that contains all active rules, used to dynamically add and remove rules from the UI
        // As they are generated and expired. ObservableCollection is used so that the UI
        // can automatically update when the collection changes.
        private ObservableCollection<Rules> activeRules = new ObservableCollection<Rules>();

        // A property used to indicate whether the challenge
        // is currently active or not, this will be used to pause and unpause all timers.
        private bool isChallengeActive = false;

        public MainWindow()
        {

            InitializeComponent();

            // Call the GenerateInitialRules method to create the first 3 random rules when the main window initializes
            GenerateInitialRules();

            //connect the activeRules collection to the ListBox in the UI
            activeRulesList.ItemsSource = activeRules;
        }

        // A simple array of all possible actions which can trigger a consequence during a timing challenge.
        // I personally enjoy nuzlockes those are always fun to watch on youtube.
        private string[] possibleActions = new string[] 
        {

            "If you touch fire",
            "If you kiss a frog",
            "If you screw the pooch",
            "If you smoke a mysterious green plant",
            "If you eat a poisonous mushroom",
            "If you fall asleep",
            "If you die",
            "If you eat a human corpse",
            "If you sacrifice a black cat",
            "If you meet a mermaid"
        };

        // A simple array of all possible consequences which can be triggered by a specific action during a timing challenge.
        private string[] possibleConsequences = new string[]
        {
            "you must discard three shields",
            "you must fight all enemies without any armor",
            "you must fight all enemies without any weapons",
            "you must play using your feet",
            "you must play blindfolded",
            "you cannot use any healing items",
            "you must run away from all enemies",
            "you must discard all your items but one",
            "you must restart from your last checkpoint"
        };

        // I need an object for random number generation to select a random action and consequence from the arrays above.
        // As well as for setting timers
        private Random randomCounter = new Random();


        // Now when the program starts I want to generate 3 random Rules to be displayed in the UI.
        // Each rule will have a timer of 5,10, and 15 minutes respectively.
        // When the timer runs out, the rule will be removed from the UI and a new random rule will be generated.
        // Each rule should derive from my Rules class
        private Rules CreateRandomRule(int timeLimit)
        {
            // Generate a random action and consequence from the arrays above
            string action = possibleActions[randomCounter.Next(possibleActions.Length)];
            string consequence = possibleConsequences[randomCounter.Next(possibleConsequences.Length)];
            // Create a new Rule object with the random action, consequence, and time limit
            Rules newRule = new Rules(timeLimit, action, consequence);
            return newRule;
        }

        // A function for the first 3 random rules, ill call this when main window initializes
        private void GenerateInitialRules()
        {
            // Create 3 random rules with time limits of 5, 10, and 15 minutes respectively
            activeRules.Add(CreateRandomRule(300));
            activeRules.Add(CreateRandomRule(600));
            activeRules.Add(CreateRandomRule(900));
        }

        // Now I need to create a function that will be called when a rule's timer runs out,
        // this function will remove the rule from the UI and generate a new random rule with the same time limit.
        private void ReplaceExpiredRule(Rules expiredRule)
        {
            // Remove the expired rule from the active rules list
            activeRules.Remove(expiredRule);
            // Generate a new random rule with the same time limit
            activeRules.Add(CreateRandomRule(expiredRule.InitialTimeLimit));
        }

        // I need a timer to keep track of the time for all rules.
        // When the timer for a rule runs out, it will call the ReplaceExpiredRule function
        // to remove the expired rule and generate a new random rule with the same time limit.
        private void RuleCountdownTimer()
        {
            // Set the timer interval to 1 second
            ruleCountdownTimer.Interval = TimeSpan.FromSeconds(1);
            // Subscribe to the Tick event of the timer
            ruleCountdownTimer.Tick += RuleCountdownTimerTick;
            // need this boolean as true else the timer will not start and the rules will not be updated
            isChallengeActive = true;
            // Start the timer
            ruleCountdownTimer.Start();

        }

        // Now I will produce an event handler for the Tick event of the timer,
        // this will be called every second and will check if any rule's timer has run out.
        private void RuleCountdownTimerTick(object sender, EventArgs e)
        {
            // Check if the challenge is active, if not, return early
            if (!isChallengeActive)
            {
                return;
            }

            // Iterate through the active rules and decrement their time limits
            for (int ruleIndex = 0; ruleIndex < activeRules.Count; ruleIndex++)
            {
                // Here we decrement the time limit of each rule by 1 second and check if the timeLimit is =0
                // If it is we call the ReplaceRule function to remove the expired rule
                // and generate a new random rule with the same time limit.
                Rules currentRule = activeRules[ruleIndex];
                currentRule.TimeLimit--;
                // If the time limit has run out, replace the rule
                if (currentRule.TimeLimit <= 0)
                {
                    ReplaceExpiredRule(currentRule);
                }
            }
            // Refresh the ListBox to update the displayed time limits on the UI
            activeRulesList.Items.Refresh(); 
            }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            //End and exit application
            // optting for this since its a 1 window application
            Application.Current.Shutdown();

            //shutdown only 1 window
            //this.Close();

        }

        // An event handler that triggers the Rule CountdownTimer function when the Start Challenge button is clicked.
        private void ButtonStartChallenge(object sender, RoutedEventArgs e)
        {
            RuleCountdownTimer();
            // Disable the Start Challenge button after starting the challenge
            if (sender is Button startChallengeButton)
            {
                startChallengeButton.IsEnabled = false;
            }

        }

        // An event handler that will pause or unpause all active challenge timers when the Pause All Timers button is clicked.
        private void ButtonPauseTimers(object sender, RoutedEventArgs e)
        {
            isChallengeActive = !isChallengeActive;
        }

        // An event handler that will replace the last created rule with a new random rule
        // with the same time limit when the Change Last Rule button is clicked.
        private void ButtonChangeLastRule(object sender, RoutedEventArgs e)
        {
            // Grabs the highest index rule and replaces it with a new random rule with the same time limit.
            if (activeRules.Count > 0)
            {
                // grab the index of the last created rule and assign it to a variable
                Rules lastCreatedRule = activeRules[activeRules.Count - 1];
                // Call the Replace Expired rule function to remove it and generate a new random rule
                ReplaceExpiredRule(lastCreatedRule);
            }
        }
    }
}