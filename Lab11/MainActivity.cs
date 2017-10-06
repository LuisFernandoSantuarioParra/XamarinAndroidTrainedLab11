using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Complex Data;
        Validar validar;

        int Counter = 0;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnCreate");
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ///Validar actividad

            var menssage = FindViewById<TextView>(Resource.Id.message);
            validar = (Validar)this.FragmentManager.FindFragmentByTag("Validar");

            string StudentEmail = "santuarioparral@hotmail.com";
            string Password = "santuario1";
           
            //Intentar recuperar el fragmento
            if (validar == null)
            {
                //no ha sido almacenado
                validar = new Validar();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(validar, "Validar");
                FragmentTransaction.Commit();
                string myDevice = Android.Provider.Settings.Secure.
                GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                var ServiceClient = new SALLab11.ServiceClient();
                var Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
                validar.Nombre = Result.Fullname;
                validar.Codigo = Result.Status.ToString();
                validar.Token = Result.Token;
            }
            menssage.Text = $"\n{validar.ToString()}";
            /////////////////////////////////////////


            FindViewById<Button>(Resource.Id.StartActivity).Click += (sender, e) =>
            {

                var ActivityIntent = new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(ActivityIntent);
            };

            //utiliza fragment para recuperar el fragmento

            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if (Data == null)
            {
                //no ha sido almacenado
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
            }

            if (bundle != null)
            {
                Counter = bundle.GetInt("CounterValue", 0);
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance Stare");
            }

            var ClickCounter = FindViewById<Button>(Resource.Id.ClicksCounter);
            ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, Counter);
            ClickCounter.Text = $"\n{Data.ToString()}";
            ClickCounter.Click += (sender, e) =>
            {
                Counter++;
                ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, Counter);
                //modificar cualquier valor para verificar la persistencia
                Data.Real++;
                Data.Imaginary++;
                //mostrar el valor de los miembros
                ClickCounter.Text += $"\n{Data.ToString()}";

            };

            

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue",Counter);
            Android.Util.Log.Debug("Lab11Log","Activity A -OnSaveInstanceState");
            base.OnSaveInstanceState(outState);

        }
        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log","Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnResume");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStop");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnDestroy");
            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnRestart");
            base.OnRestart();
        }

       

    }
}

