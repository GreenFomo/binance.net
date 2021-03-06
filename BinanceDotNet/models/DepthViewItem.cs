﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class DepthViewItem {
        public long LastUpdateId { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        
        public string EventType { get; set; }
        public long EventTime { get; set; }  
        public string Symbol { get; set; }


        public static List<DepthViewItem> BuildFromDepth(Depth depth) {
            var results = new List<DepthViewItem>();
            
            foreach (var bid in depth.Bids) {
                results.Add(new DepthViewItem() {
                    LastUpdateId = depth.LastUpdateId,
                    Price = bid.Price,
                    Quantity = bid.Quantity,
                    Type = "bid",
                    EventTime = depth.EventTime,
                    EventType = depth.EventType,
                    Symbol = depth.Symbol
                });
            }

            foreach (var ask in depth.Asks) {
                results.Add(new DepthViewItem() {
                    LastUpdateId = depth.LastUpdateId,
                    Price = ask.Price,
                    Quantity = ask.Quantity,
                    Type = "ask",
                    EventTime = depth.EventTime,
                    EventType = depth.EventType,
                    Symbol = depth.Symbol
                });
            }

            return results;

        }
    }
}
