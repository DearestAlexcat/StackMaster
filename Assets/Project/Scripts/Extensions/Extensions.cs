using Client;
using Leopotam.EcsLite;

public static class FilterEx
{
    public static bool IsEmpty(this EcsFilter filter)
    {
        return filter.GetEntitiesCount() == 0;
    }
}

public static class EcsWorldEx
{
    public static ref T GetEntityRef<T>(this EcsWorld world, int entity) where T : struct
    {
        return ref world.GetPool<T>().Get(entity);
    }

    public static void DelEntity<T>(this EcsWorld world, int entity) where T : struct
    {
        world.GetPool<T>().Del(entity);
    }

    public static void AddEntity<T>(this EcsWorld world, int entity) where T : struct
    {
        world.GetPool<T>().Add(entity);
    }

    public static void DelayAction(this EcsWorld world, float time, System.Action action)
    { 
        ref var delayPool = ref world.GetPool<ExecutionDelay>().Add(world.NewEntity());
        delayPool.time = time;
        delayPool.action = action;
    }

    public static void DelayAddEntity<T>(this EcsWorld world, int entity, float time) where T : struct
    {
        ref var delayPool = ref world.GetPool<ExecutionDelay>().Add(world.NewEntity());
        delayPool.time = time;
        delayPool.action = () => world.GetPool<T>().Add(entity);
    }

    public static void DelayDelEntity<T>(this EcsWorld world, int entity, float time) where T : struct
    {
        ref var delayPool = ref world.GetPool<ExecutionDelay>().Add(world.NewEntity());
        delayPool.time = time;
        delayPool.action = () => world.GetPool<T>().Del(entity);
    }

    public static ref T AddEntityRef<T>(this EcsWorld world, int entity) where T : struct
    {
        return ref world.GetPool<T>().Add(entity);
    }

    public static ref T NewEntityRef<T>(this EcsWorld world) where T : struct
    {
        return ref world.GetPool<T>().Add(world.NewEntity());
    }

    public static int NewEntity<T>(this EcsWorld world) where T : struct
    {
        int entity = world.NewEntity();
        world.GetPool<T>().Add(entity);
        return entity;
    }
}