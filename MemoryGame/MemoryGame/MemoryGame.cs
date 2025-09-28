using System;
using System.Collections.Generic;

namespace MemoryGameLibrary
{
    public class MemoryGame
    {
        private List<string> _cards;       // Bộ bài
        private bool[] _matched;           // Trạng thái matched
        private int _firstIndex = -1;      // Lượt lật đầu tiên
        private int _matches = 0;          // Số cặp đúng
        private int _size;                 // Số lượng thẻ
        private DateTime _startTime;       // Bắt đầu game
        private bool _started = false;     // Trạng thái game

        public int Score { get; private set; }
        public int TimeElapsed
        {
            get
            {
                if (!_started) return 0;
                return (int)(DateTime.Now - _startTime).TotalSeconds;
            }
        }

        // Khởi tạo game với n cặp
        public void Init(int pairs)
        {
            _size = pairs * 2;
            _cards = new List<string>();
            _matched = new bool[_size];

            // sinh bộ bài (đơn giản: số 1..n)
            for (int i = 1; i <= pairs; i++)
            {
                _cards.Add(i.ToString());
                _cards.Add(i.ToString());
            }

            Shuffle();
            _matches = 0;
            Score = 1000;
            _firstIndex = -1;
            _started = true;
            _startTime = DateTime.Now;
        }

        // Xáo trộn (Fisher-Yates)
        private void Shuffle()
        {
            Random rnd = new Random();
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                string tmp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = tmp;
            }
        }

        // Lấy giá trị thẻ tại index
        public string GetCardValue(int index)
        {
            if (index < 0 || index >= _size) return null;
            return _cards[index];
        }

        // Thử lật thẻ
        // return:
        //  - null nếu chưa đủ 2 thẻ
        //  - "MATCH" nếu khớp
        //  - "FAIL" nếu sai
        public string FlipCard(int index)
        {
            if (index < 0 || index >= _size) return null;
            if (_matched[index]) return null;

            if (_firstIndex == -1)
            {
                _firstIndex = index;
                return null; // chưa có kết quả
            }
            else
            {
                string result;
                if (_cards[_firstIndex] == _cards[index])
                {
                    _matched[_firstIndex] = true;
                    _matched[index] = true;
                    _matches++;
                    result = "MATCH";
                }
                else
                {
                    result = "FAIL";
                    // trừ điểm theo thời gian + sai
                    Score = Math.Max(0, Score - 10 - TimeElapsed);
                }
                _firstIndex = -1;
                return result;
            }
        }

        // Kiểm tra thắng
        public bool IsWin()
        {
            return _matches * 2 == _size;
        }
    }
}
