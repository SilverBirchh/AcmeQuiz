using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "Acme Quizzes", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // First task is to create the question if they don't currently exist
            var quizFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Fully qualified path of the DB file
            var dbFile = Path.Combine(quizFolder, "Quiz.sqlite");

            // Checks if the file already exists on the users device
            if (!System.IO.File.Exists(dbFile))
            {
                // File does not exist so create it
                var database = Resources.OpenRawResource(Resource.Raw.Quiz);
                FileStream writeStream = new FileStream(dbFile,
                                                        FileMode.OpenOrCreate,
                                                        FileAccess.Write);
                ReadWriteStream(database, writeStream);
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Grab references to the buttons on the front page
            Button instructionsBtn = FindViewById<Button>(Resource.Id.GoInstructions);
            Button startQuizBtn = FindViewById<Button>(Resource.Id.StartQuiz);
            Button adminBtn = FindViewById<Button>(Resource.Id.admin);

            // Create Click handler to take the user to the PreQuiz page
            startQuizBtn.Click += delegate
            {
                Intent preQuizIntent = new Intent(this, typeof(PreQuizActivity));
                StartActivity(preQuizIntent);
            };

            // Create Click handler to take the user to the Instructions page
            instructionsBtn.Click += delegate
            {
                Intent instructionsIntent = new Intent(this, typeof(InstructionsActivity));
                StartActivity(instructionsIntent);
            };

            // Create Click handler to take the user to the Admin page
            // TODO: Add user validation here
            adminBtn.Click += delegate
            {
                Intent adminIntent = new Intent(this, typeof(AdminActivity));
                StartActivity(adminIntent);
            };

        }

        /*
         * Method to write a file to the users device
         */
        private void ReadWriteStream(Stream readStream, FileStream writeStream)
        {
            int length = 256;
            byte[] buffer = new byte[length];
            int bytesRead = readStream.Read(buffer, 0, length);

            // Write the bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, length);
            }

            readStream.Close();
            writeStream.Close();
        }
    }
}

