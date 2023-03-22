using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFFoodPandaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(
                "https://localhost:7136/api/GameQueryServies");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            label1.Text = json;


            List<CGame> games = JsonSerializer.Deserialize<List<CGame>>(json);
            dataGridView1.DataSource = games;

        }
    }
}
