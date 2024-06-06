namespace task4_1.Models;

public class Database
{
    private readonly SQLiteConnection _connection;
    public Database()
    {
        
        string dbName = "FarmDataOriginal.db";
        string dbPath = Path.Combine(Current.AppDataDirectory, dbName);
        
        if (!File.Exists(dbPath))
        {
            //open the file from asset folder 
            using Stream stream = Current.OpenAppPackageFileAsync(dbName).Result;
            using MemoryStream memorystream = new();
            stream.CopyTo(memorystream);
            //write db data to app directory
            File.WriteAllBytes(dbPath, memorystream.ToArray());
        }
        _connection = new SQLiteConnection(dbPath);//initialize the connection
        _connection.CreateTables<Cow,Sheep>();//create tables if they dont exist
                                                         //, if they do ignore
    }
    public List<Animal> ReadItems()
    {
        var animals = new List<Animal>();
        var lst1 = _connection.Table<Cow>().ToList();
        animals.AddRange(lst1);
        var lst2 = _connection.Table<Sheep>().ToList();
        animals.AddRange(lst2);
        return animals;
    }
    public int InsertItem(Animal item)
    {
        return _connection.Insert(item);
    }
    public int DeleteItem(Animal item)
    {
        return _connection.Delete(item);
    }
    public int UpdateItem(Animal item)
    {
        return _connection.Update(item);
    }
}
