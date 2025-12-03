using System;
using System.Collections.Generic;
using AwesomeRPG.UI.Elements;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Components;


public abstract class ComponentBase : ElementBase
{
    private ElementBase _ComponentRootElement;
    public ElementBase ComponentRootElement {
        get
        {
            return _ComponentRootElement;
        } 
        protected set
        {
            if (_ComponentRootElement != null)
            {
                _ComponentRootElement.UndoUseAsComponentRoot(this);
            }
            _ComponentRootElement = value;
            if (value != null)
            {
                _ComponentRootElement.UseAsComponentRoot(this);
            }
        }
    }

    public override Rectangle OffsetAndSize { get => ComponentRootElement.OffsetAndSize; set => ComponentRootElement.OffsetAndSize = value; }
    protected internal override void Draw(GameTime gameTime)
    {
        ComponentRootElement.CalculateDerivedValuesFromAncestors();
        ComponentRootElement.Draw(gameTime);
    }
    protected internal override void Update(GameTime gameTime)
    {
        ComponentRootElement.CalculateDerivedValuesFromAncestors();
        ComponentRootElement.Update(gameTime);
    }
    public new void AddActionOnUIEvent(UIEvent uiEvent, Action<UIEventParamsBase> action)
    {
        ComponentRootElement._registeredUiEventActions[uiEvent].Add(action);
    }
    public new void RemoveActionOnUIEvent(UIEvent uiEvent, Action<UIEventParamsBase> action)
    {
        ComponentRootElement._registeredUiEventActions[uiEvent].Remove(action);
    }
    public new void DispatchUIEvent(UIEvent uiEvent, UIEventParamsBase uiEventInfo)
    {
        foreach (Action<UIEventParamsBase> uiAction in ComponentRootElement._registeredUiEventActions[uiEvent])
        {
            uiAction(uiEventInfo);
        }
    }
}