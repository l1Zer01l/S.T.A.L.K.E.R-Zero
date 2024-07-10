/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace StalkerZero.Infrastructure.Reactive
{
    public static class ReactivePropertyExtention
    {

        //ReactiveProperty
        public static void UpdateValue(this ReactiveProperty<int> reactiveProperty, object sender, int value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }

        public static void UpdateValue(this ReactiveProperty<float> reactiveProperty, object sender, float value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }

        public static void UpdateValue(this ReactiveProperty<double> reactiveProperty, object sender, double value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }

        public static void UpdateValue(this ReactiveProperty<string> reactiveProperty, object sender, string value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }

        public static void Opposed(this ReactiveProperty<bool> reactiveProperty, object sender)
        {
            reactiveProperty.SetValue(sender, !reactiveProperty.Value);
        }


        //SingleReactiveProperty
        public static void UpdateValue(this SingleReactiveProperty<int> reactiveProperty, object sender, int value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }

        public static void UpdateValue(this SingleReactiveProperty<float> reactiveProperty, object sender, float value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }

        public static void UpdateValue(this SingleReactiveProperty<double> reactiveProperty, object sender, double value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }

        public static void UpdateValue(this SingleReactiveProperty<string> reactiveProperty, object sender, string value)
        {
            reactiveProperty.SetValue(sender, reactiveProperty.Value + value);
        }
        public static void Opposed(this SingleReactiveProperty<bool> reactiveProperty, object sender)
        {
            reactiveProperty.SetValue(sender, !reactiveProperty.Value);
        }
    }
}
