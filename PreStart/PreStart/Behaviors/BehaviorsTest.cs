
using Xamarin.Forms;

namespace PreStart.Behaviors
{

    public class BehaviorsTest : Behavior<Entry>
    {


        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);

        }
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);

        }

        // Behavior implementation
        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            string empty = "";
            bool isValid = true;
            if (args.ToString().Length != empty.Length & isValid)
            {
                ((Entry) sender).BackgroundColor = Color.Red;
                isValid = false;
            }
            else
            {
                ((Entry)sender).BackgroundColor = Color.Aqua;
                isValid = true;
            }

        }
    }

}