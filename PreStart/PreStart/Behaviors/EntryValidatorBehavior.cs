﻿using System;
using Xamarin.Forms;

namespace PreStart.Behaviors
{
    public class EntryValidatorBehavior : Behavior<Entry>
    {
        //an IsValid property that is backed by a bindable property
        //it is a read-only bindable property.
        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("isValid",
                typeof(bool),
                typeof(BehaviorsTest),
                false);

        public static readonly BindableProperty IsValidProperty =
            IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            private set { SetValue(IsValidPropertyKey, value); }
            get { return (bool) GetValue(IsValidProperty); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(entry);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = ((Entry) sender);
            IsValid = IsValidEntry(entry.Text);
        }

        bool IsValidEntry(string entryText)
        {
            if (String.IsNullOrEmpty(entryText) | String.IsNullOrWhiteSpace(entryText))
            {
                return false;
            }

            return true;
        }
    }
}
