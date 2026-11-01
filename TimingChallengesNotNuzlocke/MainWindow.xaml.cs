// Filename: MainWindow.xaml.cs
// Author: Andrew Filson Jr
// Date: 2026-11-01
// DESC: This class contains the main logic of this TimingChallengesNotNuzlocke project. And data for Actions and Consequences.

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

        public MainWindow()
        {

            InitializeComponent();
            // Call the GenerateInitialRules method to create the first 3 random rules when the main window initializes
            GenerateInitialRules();
        }

        // A simple array of all possible actions which can trigger a consequence during a timing challenge.
        // I personally enjoy nuzlockes those are always fun to watch on youtube.
        private string[] possibleActions = new string[] {

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
            Rules ruleOne = CreateRandomRule(300);
            Rules rulTwo = CreateRandomRule(600);
            Rules ruleThree = CreateRandomRule(900);
        }

        // Now I need to create a function that will be called when a rule's timer runs out,
        // this function will remove the rule from the UI and generate a new random rule with the same time limit.
        private void ReplaceRule(Rules expiredRule)
        {
       
        }

        // I need a timer to keep track of the time for all rules.
        // When the timer for a rule runs out, it will call the ReplaceRule function
        // to remove the expired rule and generate a new random rule with the same time limit.
        private void RuleCountdownTimer()
        {
            // Set the timer interval to 1 second
            ruleCountdownTimer.Interval = TimeSpan.FromSeconds(1);
            // Subscribe to the Tick event of the timer
            ruleCountdownTimer.Tick += RuleCountdownTimer_Tick;
            // Start the timer
            ruleCountdownTimer.Start();

        }

        // Now I will produce an event handler for the Tick event of the timer,
        // this will be called every second and will check if any rule's timer has run out.
        private void RuleCountdownTimer_Tick(object sender, EventArgs e)
        {
            // This fundamentally only works to track all rules at once using a collection rather than an array
            // The collection must contain all rules and allow me to add and remove rules as they are generated and expired.
            // Will need to circle back once I figure out how to add rules1,2,3 into a collection and not an array
        }

    }
}