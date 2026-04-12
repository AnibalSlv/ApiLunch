using ApiLunch.Dto;
    
namespace ApiLunch.DataBase;
    
public interface IDataBase
{
    public void AddDataDb(AddApiDto addApiDto);
    public List<AddApiDto> GetDataDb { get; }
    public int Count { get; }
}