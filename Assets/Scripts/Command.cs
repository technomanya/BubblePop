using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Command
{
    private bool _isExecuting = false;

    public bool isExecuting => _isExecuting;

    public async void Execute()
    {
        _isExecuting = true;
        await AsyncExecute();
        _isExecuting = false;

    }

    protected abstract Task AsyncExecute();
}
