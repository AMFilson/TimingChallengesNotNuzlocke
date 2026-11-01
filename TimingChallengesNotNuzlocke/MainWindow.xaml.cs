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
// Need to add this reference else it cant find my Rules.cs class
using TimingChallengesNotNuzlocke.TimingChallengeClasses;

namespace TimingChallengesNotNuzlocke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
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
        private Rules createRandomRule(int timeLimit)
        {
            // Generate a random action and consequence from the arrays above
            string action = possibleActions[randomCounter.Next(possibleActions.Length)];
            string consequence = possibleConsequences[randomCounter.Next(possibleConsequences.Length)];
            // Create a new Rule object with the random action, consequence, and time limit
            Rules newRule = new Rules(timeLimit, action, consequence);
            return newRule;
        }

        // A function for the first 3 random rules, ill call this when main window initializes
        private void generateInitialRules()
        {
            // Create 3 random rules with time limits of 5, 10, and 15 minutes respectively
            Rules ruleOne = createRandomRule(300);
            Rules rulTwo = createRandomRule(600);
            Rules ruleThree = createRandomRule(900);


    }

}