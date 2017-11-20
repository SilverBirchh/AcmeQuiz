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
            var QuizFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Fully qualified path of the DB file
            var DbFile = Path.Combine(QuizFolder, "Quiz.sqlite");

            // Checks if the file already exists on the users device
            if (!System.IO.File.Exists(DbFile))
            {
                // File does not exist so create it
                var Database = Resources.OpenRawResource(Resource.Raw.Quiz);
                FileStream WriteStream = new FileStream(DbFile,
                                                        FileMode.OpenOrCreate,
                                                        FileAccess.Write);
                ReadWriteStream(Database, WriteStream);
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Grab references to the buttons on the front page
            Button InstructionsBtn = FindViewById<Button>(Resource.Id.GoInstructions);
            Button StartQuizBtn = FindViewById<Button>(Resource.Id.StartQuiz);
            Button AdminBtn = FindViewById<Button>(Resource.Id.admin);

            // Create Click handler to take the user to the PreQuiz page
            StartQuizBtn.Click += delegate
            {
                Intent PreQuizIntent = new Intent(this, typeof(PreQuizActivity));
                StartActivity(PreQuizIntent);
            };

            // Create Click handler to take the user to the Instructions page
            InstructionsBtn.Click += delegate
            {
                Intent InstructionsIntent = new Intent(this, typeof(InstructionsActivity));
                StartActivity(InstructionsIntent);
            };

            // Create Click handler to take the user to the Admin page
            // TODO: Add user validation here
            AdminBtn.Click += delegate
            {
                Intent AdminIntent = new Intent(this, typeof(AdminActivity));
                StartActivity(AdminIntent);
            };

        }

        /*
         * Method to write a file to the users device
         */
        private void ReadWriteStream(Stream readStream, FileStream writeStream)
        {
            int length = 256;
            byte[] buffer = new byte[length];
            int BytesRead = readStream.Read(buffer, 0, length);

            // Write the bytes
            while (BytesRead > 0)
            {
                writeStream.Write(buffer, 0, BytesRead);
                BytesRead = readStream.Read(buffer, 0, length);
            }

            readStream.Close();
            writeStream.Close();
        }
    }
}

