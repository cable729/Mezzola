namespace Mezzola.Engine.Unit
{
    public interface IHealth
    {
        int CurrentHealth { get; set; }
        int MaxHealth { get; set; }
    }
}