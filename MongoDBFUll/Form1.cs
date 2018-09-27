using System;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoDBFUll
{

    class Person
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Phones { get; set; }
        public bool IsActive { get; set; }

    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            MongoClientSettings user = new MongoClientSettings()
            {
                Server = new MongoServerAddress("www.epocum.com", 27017),
                Credentials = new MongoCredential[] { MongoCredential.CreateCredential("admin", "mimonenew", "mimonenewpass") }
            };

            var client = new MongoClient(user);
            var database = client.GetDatabase("admin");
            var collection = database.GetCollection<BsonDocument>("persone");

            var list = await collection.Find(new BsonDocument()).ToListAsync();

            dataGridView1.Columns.Add("name", "Nome");
            dataGridView1.Columns.Add("tel", "Telefono");
            dataGridView1.Columns.Add("att", "Attivo");

            foreach (var p in list)
            {
                dataGridView1.Rows.Add(p[1], p[2], p[3]);
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            MongoClientSettings user = new MongoClientSettings()
            {
                Server = new MongoServerAddress("www.epocum.com", 27017),
                Credentials = new MongoCredential[] { MongoCredential.CreateCredential("admin", "mimonenew", "mimonenewpass") }
            };

            var client = new MongoClient(user);
            var database = client.GetDatabase("admin");
            var collection = database.GetCollection<Person>("persone");

            var persona = new Person { Name = txtName.Text, Phones =  txtPhone.Text, IsActive = true};
            await collection.InsertOneAsync(persona);

            var collection2 = database.GetCollection<BsonDocument>("persone");
            var list = await collection2.Find(new BsonDocument()).ToListAsync();

            createView();
            foreach (var p in list)
            {
                dataGridView1.Rows.Add(p[1], p[2], p[3]);
            }
            dataGridView1.Refresh();
        }

        void createView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("name", "Nome");
            dataGridView1.Columns.Add("tel", "Telefono");
            dataGridView1.Columns.Add("att", "Attivo");
            dataGridView1.Rows.Clear();
        }

        private void DB_create_Collection()
        {

            MongoClientSettings user = new MongoClientSettings()
            {
                Server = new MongoServerAddress("www.epocum.com", 27017),
                Credentials = new MongoCredential[] { MongoCredential.CreateCredential("admin", "mimonenew", "mimonenewpass") }
            };

            var client = new MongoClient(user);
            var database = client.GetDatabase("admin");
            database.CreateCollection("test");
            Console.WriteLine("Create!");
        }
    }
}