namespace ApiLunch.DataBase;
using ApiLunch.Dto;

class ListDataBase : IDataBase
{
    private List<AddApiDto> _listDb = new();

    public void AddDataDb(AddApiDto addApiDto)
    {
        _listDb.Add(addApiDto);
    }

    public List<AddApiDto> GetDataDb => _listDb;
    
    public int Count => _listDb.Count; // esto es como decir: Count { get{ return _listDb.Count} }
}
