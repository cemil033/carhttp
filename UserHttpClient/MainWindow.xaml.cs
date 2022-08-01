using CarHttpServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserHttpClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HttpClient client=new HttpClient();
        
        public MainWindow()
        {
            InitializeComponent();           

        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            var user =new User() { Id=int.Parse(tb_id.Text),Name=tb_name.Text,Surname=tb_surname.Text};
            var json=JsonConvert.SerializeObject(user);
            var t = client.PostAsync("http://localhost:63291/", new StringContent(json, Encoding.UTF8, "application/json"));
            var stringContent = t.Result.Content.ReadAsStringAsync().Result;
            MessageBox.Show(stringContent);
        }

        List<User> users = new List<User>();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var t = client.GetAsync("http://localhost:63291/");
            var stringContent = t.Result.Content.ReadAsStringAsync().Result;
            users=JsonConvert.DeserializeObject<List<User>>(stringContent);
            Lst_t.ItemsSource = users;
        }
    }
}
