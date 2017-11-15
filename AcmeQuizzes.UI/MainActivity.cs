using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;
using System;
using AcmeQuizzes;
using System.Collections.Generic;

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
            // Name of the file
            var DbFile = Path.Combine(QuizFolder, "Quiz.sqlite");

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
            Button Instructions = FindViewById<Button>(Resource.Id.GoInstructions);
            Button StartQuiz = FindViewById<Button>(Resource.Id.StartQuiz);

            // Create Click handler to take the user to the PreQuiz page
            StartQuiz.Click += delegate
            {
                Intent GoPreQuiz = new Intent(this, typeof(PreQuiz));
                StartActivity(GoPreQuiz);
            };

            // Create Click handler to take the user to the Instructions page
            Instructions.Click += delegate
            {
                Intent GoInstructions = new Intent(this, typeof(Instructions));
                StartActivity(GoInstructions);
            };


        }

        private void ReadWriteStream(Stream database, FileStream writeStream)
        {
            int length = 256;
            byte[] buffer = new byte[length];
            int BytesRead = database.Read(buffer, 0, length);

            // Write the bytes
            while (BytesRead > 0)
            {
                writeStream.Write(buffer, 0, BytesRead);
                BytesRead = database.Read(buffer, 0, length);
            }

            database.Close();
            writeStream.Close();
        }
    }
}

