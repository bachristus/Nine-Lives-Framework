using System;
using System.Collections.Generic;

namespace NineLives.Framework.Core.Progress
{
    public class WeightedProgressAggregator:IOperationProgressReporter
    {
        private class Item
        {
            public float Weight;
            public OperationProgressData ProgressData;
        }

        private readonly List<Item> _items = new();

        public event Action<OperationProgressData>? Changed;

        public IOperationProgress CreateSubProgress(float weight)
        {
            var item = new Item { Weight = weight, ProgressData = default };
            _items.Add(item);

            return new SubProgress(item, OnSubProgressChanged);
        }

        private float CalculateTotal()
        {
            float totalWeight = 0f;
            float sum = 0f;

            foreach (var item in _items)
            {
                totalWeight += item.Weight;
                sum += item.ProgressData.progress01 * item.Weight;
            }

            return totalWeight == 0 ? 0 : sum / totalWeight;
        }

        public void Report(OperationProgressData _)
        {            
            throw new NotSupportedException("Do not use this method directly. Use SubProgress only");
        }

        private void OnSubProgressChanged(OperationProgressData subProgressData)
        {
            float totalProgress = CalculateTotal();
            var data = new OperationProgressData(totalProgress,subProgressData.currentOperation); 
            Changed?.Invoke(data);
        }        

        private class SubProgress : IOperationProgress
        {
            private readonly Item _item;
            private readonly Action<OperationProgressData> _notify;

            public SubProgress(Item item, Action<OperationProgressData> notify)
            {
                _item = item;
                _notify = notify;
            }

            public void Report(OperationProgressData data)
            {
                _item.ProgressData = data;
                _notify(data);
            }

            public void Report(float progress, string operationStage)
            {
                Report(new OperationProgressData(progress, operationStage));
            }
        }
    }
}