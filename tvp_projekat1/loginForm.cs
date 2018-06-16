using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Windows.Forms;

namespace tvp_projekat1
{
    public partial class loginForm : Form
    {
        private adminForm adminForm;
        private studentForm studentForm;
        private bool incorrectPwdFlag = true;

        public loginForm()
        {
            InitializeComponent();
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            IMongoCollection<BsonDocument> collection = new MongoClient("mongodb://sdee3:sdee3@ds147964.mlab.com:47964/tvp")
                .GetDatabase("tvp")
.GetCollection<BsonDocument>("login");
                
            loginButton.Click += (s, EventArgs) => { ProveriPodatke(s, EventArgs, collection.Find(new BsonDocument()).ToCursor()); };
        }

        private void ProveriPodatke(object sender, EventArgs e, IAsyncCursor<BsonDocument> asyncCursor)
        {
            foreach (BsonDocument document in asyncCursor.ToEnumerable())
            {
                if (document["username"].Equals(usernameInput.Text) && document["password"].Equals(passwordInput.Text))
                    if (document["accountStatus"].Equals("admin"))
                    {
                        incorrectPwdFlag = false;
                        adminForm = new adminForm();
                        this.Hide();
                        adminForm.Closed += (s, args) => this.Close();
                        adminForm.Show();
                    }
                    else if (document["accountStatus"].Equals("student"))
                    {
                        incorrectPwdFlag = false;
                        studentForm = new studentForm(document["username"].ToString());
                        this.Hide();
                        studentForm.Closed += (s, args) => this.Close();
                        studentForm.Show();
                    }
            }

            if (incorrectPwdFlag)
                MessageBox.Show("Neispravno uneti podaci! Pokušajte ponovo!");
        }

    }
}