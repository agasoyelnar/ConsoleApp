using DomainLayer.Common;

namespace DomainLayer.Entities;

public class Group : BaseEntity
{
    public  string Name { get; set; }
    public  string Teacher { get; set; }
    public int RoomCount { get; set; }
}