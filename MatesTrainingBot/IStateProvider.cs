namespace MatesTrainingBot;

public interface IStateProvider<out TState, in TKey>
{
    TState? GetState(TKey key, CancellationToken cancellationToken);
}