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
// Just telling it where to go, ie. referencing the namespace which is defined at the top of the Rules.cs file
using TimingChallengesNotNuzlocke.TimingChallengeClasses;

namespace TimingChallengesNotNuzlocke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        // A collection that contains all active rules, used to dynamically add and remove rules from the UI
        // As they are generated and expired. ObservableCollection is used so that the UI
        // can automatically update when the collection changes.
        private ObservableCollection<Rules> activeRules = new ObservableCollection<Rules>();

        // A property used to indicate whether the challenge
        // is currently active or not, this will be used to pause and unpause all timers.
        private bool isChallengeActive = false;

        // 3 properties for the time limits of the 3 rules so I can remove the hardcoded values
        // and make it easier to change them in the future.
        // 5min in seconds
        private const int FirstRuleTimeLimit = 300;
        // 10min in seconds
        private const int SecondRuleTimeLimit = 600;
        // 15min in seconds
        private const int ThirdRuleTimeLimit = 900;

        public MainWindow()
        {

            InitializeComponent();

            // Call the GenerateInitialRules method to create the first 3 random rules when the main window initializes
            GenerateInitialRules();

            //connect the activeRules collection to the ListBox in the UI
            activeRulesList.ItemsSource = activeRules;
        }

        // A one dimensional array of all possible actions which can trigger a consequence during a timing challenge.
        // I personally enjoy nuzlockes those are always fun to watch on youtube.
        // In the future I'd like to add positive consequences as well
        private string[] possibleActions = new string[]
        {

            "If you touch fire",
            "If you kiss a frog",
            "If you screw the pooch",
            "If you smoke a mysterious green plant",
            "If you eat a poisonous mushroom",
            "If you fall",
            "If you die",
            "If you drink a potion",
            "If you hurt a cat",
            "If you meet a mermaid"
        };

        // A simple array of all possible consequences which can be triggered by a specific action during a timing challenge.
        private string[] possibleConsequences = new string[]
        {
            "discard three shields",
            "fight without any armor",
            "fight without any weapons",
            "play using your feet",
            "play blindfolded",
            "discard all healing items",
            "run away from all enemies",
            "discard all items but one",
            "restart from your last checkpoint"
        };

        // I need an object for random number generation to select a random action and consequence from the arrays above.
        // As well as for setting timers
        // set to readonly no need for anyone to be able to reassign
        private readonly Random randomNumberGenerator = new Random();


        // Now when the program starts I want to generate 3 random Rules to be displayed in the UI.
        // Each rule will have a timer of 5,10, and 15 minutes respectively.
        // When the timer runs out, the rule will be removed from the UI and a new random rule will be generated.
        // Each rule should derive from my Rules class
        private Rules CreateRandomRule(int timeLimit)

           
        {
            // Generate a random action and consequence from the arrays above
            // In the existing form the strings below utilize Next(int maxValue) overload
            // omitting a requirement to otherwise implement selection statements and error handling
            string action = possibleActions[randomNumberGenerator.Next(possibleActions.Length)];
            string consequence = possibleConsequences[randomNumberGenerator.Next(possibleConsequences.Length)];
            // Create a new Rule object with the random action, consequence, and time limit
            Rules newRule = new Rules(timeLimit, action, consequence);
           
            return newRule;
        }

        // A function for the first 3 random rules, ill call this when main window initializes
        // This can be easily converted into an AddRule() function in the future but I will want to define a limit for  of additions
        // my own understanding and simplicity.
        private void GenerateInitialRules()
        {
            // Create 3 random rules with modular time limits and insert the values into the activeRules collection
            activeRules.Add(CreateRandomRule(FirstRuleTimeLimit));
            activeRules.Add(CreateRandomRule(SecondRuleTimeLimit));
            activeRules.Add(CreateRandomRule(ThirdRuleTimeLimit));
        }

        // a function that will be called when a rule's timer runs out,
        // this function will remove the rule from the UI and generate a new random rule with the same time limit.
        private void ReplaceExpiredRule(Rules expiredRule)
        {
            // Remove the expired rule from the active rules list
            activeRules.Remove(expiredRule);
            // Generate a new random rule object with the same time limit as the rule that just expired
            activeRules.Add(CreateRandomRule(expiredRule.InitialTimeLimit));
        }

        // use the DispatcherTimer class to create a timer that will decrement the time limit of each rule every second.
        private DispatcherTimer ruleCountdownTimer = new DispatcherTimer();

        // Decrement rate in seconds for RuleCountdownTimer
        private int decrementRate = 1;

        // Start the countdown timer for all active rules, this will be called when the Start Challenge button is clicked.
        // When the timer for a rule runs out, it will call the ReplaceExpiredRule function
        // to remove the expired rule and generate a new random rule with the same time limit
        // Basically just runs a timer that uses the RuleCountdownTimerTick() function every 1 second to
        // decrement the value of each rules TimeLimit by 1, every 1 second.
        private void StartRuleCountdownTimer()
        {
            try
            {
                // I'm placing a try catch here since the to catch any ArgumentOutofRange exceptions That could occur 
                // Set the timer interval to whatever in seconds using FromSeconds() method
                ruleCountdownTimer.Interval = TimeSpan.FromSeconds(decrementRate);
                // Subscribe to the Tick event of the timer
                ruleCountdownTimer.Tick += RuleCountdownTimerTick;
                // need this boolean as true else the timer will not start and the rules will not be updated
                isChallengeActive = true;
                // Start the timer
                ruleCountdownTimer.Start();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"Wallahi bro, an ArgumentOutOfRange exception occurred: {ex.Message}");

            }
        }

        // an event handler for the Tick event of the timer,
        // this will be called every second and will check if any rule's timer has run out.
        private void RuleCountdownTimerTick(object sender, EventArgs e)
        {
            // Check if the challenge is active, if so it decrementsbased off the decrementRate variable else it remains paused
            if (!isChallengeActive)
            {
                return;
            }

            // Basic exception handling for any errors I happened to miss when trying to decrement the timers for the rules
            try
            {   // Iterate through the active rules and decrement their time limits
                // There should be no way to get an index out of range error here since
                // the for loop is based off the activeRules.Count property and index starts at 0 to Count -1
                // but exception handling is used here to display applicability and error 
                for (int individualActiveRule = 0; individualActiveRule < activeRules.Count; individualActiveRule++)
                {
                    // Here we decrement the time limit of each rule and check if the timeLimit is <= 0
                    // If it is we call the ReplaceRule function to remove the expired rule
                    // and generate a new random rule with the same time limit.
                    Rules currentRule = activeRules[individualActiveRule];
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
            catch (Exception ex)
            {
                // I'd like to know exactly where I messed up
                MessageBox.Show($"An error occurred with the RuleCountdownTimerTick() function: {ex.Message}");
            }
        }

        // An event handler that shuts down the application when the Close button is clicked
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
            // Set the parameters, create a timer, and start the countdown for all rules.
            StartRuleCountdownTimer();
            // Disable the Start Challenge button after starting the challenge
            if (sender is Button startChallengeButton)
            {
                startChallengeButton.IsEnabled = false;
            }

        }

        // An event handler that will pause or unpause all active challenge timers when the Pause All Timers button is clicked.
        private void ButtonPauseTimers(object sender, RoutedEventArgs e)
        {
            //flip flop, hip hop
            isChallengeActive = !isChallengeActive;
        }

        // An event handler that will replace the last created rule with a new random rule
        // with the same time limit when the Change Last Rule button is clicked.
        private void ButtonChangeLastRule(object sender, RoutedEventArgs e)
        {
            // Basic exception handling to catch any potential errors that may occur during the replacement process
            try
            {
                // Grabs the highest index rule and replaces it with a new random rule with the same time limit.
                if (activeRules.Count > 0)
                {
                    // grab the the last created rule based off index and assign it to a variable
                    Rules lastCreatedRule = activeRules[activeRules.Count - 1];
                    // Call the Replace Expired rule function to remove it and generate a new random rule
                    ReplaceExpiredRule(lastCreatedRule);
                }
                else
                {
                    MessageBox.Show("No active rules to replace.");
                }
            }

            // If for whatever reason the index is out of range, display an explicit message to indicate that
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show($"Index out of range error: {ex.Message}");
            }
            // General exception handling to catch any unexpected errors that may occur
            // during the replacement process
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}