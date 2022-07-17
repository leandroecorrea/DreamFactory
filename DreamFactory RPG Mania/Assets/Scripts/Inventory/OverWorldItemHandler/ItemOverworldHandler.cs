public abstract class ItemOverworldHandler
{
    protected Item _item;

    public ItemOverworldHandler(Item item)
    {
        _item = item;
    }
    public abstract void Handle(PlayerPartyMemberConfig member);    
}