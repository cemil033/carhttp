using CarHttpServer;
using Newtonsoft.Json;
using System.Net;


List<User> users = new List<User>()
{
    new User(){ Name = "UserOne",Surname="UserOne",Id=1},
    new User(){ Name = "User2",Surname="User2",Id=2},
    new User(){ Name = "User3",Surname="User3",Id=3},
    new User(){ Name = "User4",Surname="User4",Id=4},
    new User(){ Name = "User5",Surname="User5",Id=5}
};
HttpListener listener = new();
listener.Prefixes.Add("http://localhost:63291/");
listener.Start();
while (true)
{
    var context = listener.GetContext(); 
    var request = context.Request;
    var response = context.Response;
    if (request.HttpMethod == "GET")
    {
        StreamWriter sw = new(response.OutputStream);
        sw.WriteLine(JsonConvert.SerializeObject(users));
        sw.Close();
    }
    else if (request.HttpMethod == "POST")
    {
        StreamReader sr = new(request.InputStream);
        var cnt = sr.ReadToEnd();
        users.Add(JsonConvert.DeserializeObject<User>(cnt));
        sr.Close();

        StreamWriter sw = new(response.OutputStream);
        sw.WriteLine("OK");
        sw.Close();
    }

}