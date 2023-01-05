using LastProjectVersion2;
using LastProjectVersion2.Models;

public static class Program
{
    public static void Main()
    {
        Area51 area51 = new Area51();
        Thread thread = new Thread(area51.Start);
        thread.Start();

    }
}
