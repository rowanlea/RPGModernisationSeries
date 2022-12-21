namespace DatabaseSetup.TableData
{
    internal class ItemTypeData
    {
        internal static string Query = @"IF((SELECT count(*) FROM ItemType)=0) BEGIN insert into ItemType (ID, Type) values (1, 'Equip');
insert into ItemType (ID, Type) values (2, 'Use');
insert into ItemType (ID, Type) values (3, 'Etc'); END";
    }
}
