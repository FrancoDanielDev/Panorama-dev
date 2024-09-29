public abstract class PlayerControllers
{
    protected Player player;

    public PlayerControllers(Player player)
    {
        this.player = player;
    }

    public abstract void Start();

    public abstract void Update();
}
