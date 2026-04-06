namespace ApiLunch.DataBase;
using ApiLunch.Dto;

class ListDataBase
{
    private List<AddApiDto> _listDb = new();

    public void AddDataDb(AddApiDto dto)
    {
        _listDb.Add(dto);
    }

    public List<AddApiDto> GetDataDb => _listDb;
    
    public int Count => _listDb.Count; // esto es como decir: Count { get{ return _listDb.Count} }
}
