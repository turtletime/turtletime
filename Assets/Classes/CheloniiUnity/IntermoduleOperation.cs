using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheloniiUnity
{
    class IntermoduleOperation
    {
        public GameModuleKey Source { get; private set; }
        public GameModuleKey Target { get; private set; }
        public Action SelectedAction { get; private set; }

        public static IntermoduleOperation CreateSuspendOperation(GameModuleKey source, GameModuleKey target)
        {
            return IntermoduleOperationBuilder.Create().SetSource(source).SetTarget(target).SetAction(Action.SUSPEND).Build();
        }

        public static IntermoduleOperation CreateResumeOperation(GameModuleKey source, GameModuleKey target)
        {
            return IntermoduleOperationBuilder.Create().SetSource(source).SetTarget(target).SetAction(Action.RESUME).Build();
        }

        public static IntermoduleOperation CreateOpenChannelOperation(GameModuleKey source, GameModuleKey target)
        {
            return IntermoduleOperationBuilder.Create().SetSource(source).SetTarget(target).SetAction(Action.OPEN_CHANNEL).Build();
        }

        public static IntermoduleOperation CreateTransferDataOperation(GameModuleKey source, GameModuleKey target)
        {
            return IntermoduleOperationBuilder.Create().SetSource(source).SetTarget(target).SetAction(Action.TRANSFER_DATA).Build();
        }

        public static IntermoduleOperation CreateLoadOperation(GameModuleKey source, GameModuleKey target)
        {
            return IntermoduleOperationBuilder.Create().SetSource(source).SetTarget(target).SetAction(Action.LOAD).Build();
        }

        public static IntermoduleOperation CreateUnloadOperation(GameModuleKey source, GameModuleKey target)
        {
            return IntermoduleOperationBuilder.Create().SetSource(source).SetTarget(target).SetAction(Action.UNLOAD).Build();
        }

        private class IntermoduleOperationBuilder
        {
            public GameModuleKey source;
            public GameModuleKey target;
            public Action selectedAction;

            public static IntermoduleOperationBuilder Create()
            {
                return new IntermoduleOperationBuilder();
            }

            public IntermoduleOperationBuilder SetSource(GameModuleKey source)
            {
                this.source = source;
                return this;
            }

            public IntermoduleOperationBuilder SetTarget(GameModuleKey target)
            {
                this.target = target;
                return this;
            }

            public IntermoduleOperationBuilder SetAction(Action action)
            {
                selectedAction = action;
                return this;
            }

            public IntermoduleOperation Build()
            {
                IntermoduleOperation result = new IntermoduleOperation();
                result.Source = source;
                result.Target = target;
                result.SelectedAction = selectedAction;
                return result;
            }
        }
        
        public enum Action
        {
            SUSPEND, RESUME, LOAD, UNLOAD, OPEN_CHANNEL, TRANSFER_DATA
        }
    }
}
