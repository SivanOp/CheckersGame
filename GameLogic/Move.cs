using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameLogic
{
    public struct Move
    {
        private Point m_MoveFrom;
        private Point m_MoveTo;

        public Move(Point i_MoveFrom, Point i_MoveTo)
        {
            m_MoveFrom = i_MoveFrom;
            m_MoveTo = i_MoveTo;
        }

        public Point MoveFrom
        {
            get
            {
                return m_MoveFrom;
            }

            set
            {
                m_MoveFrom = value;
            }
        }

        public Point MoveTo
        {
            get
            {
                return m_MoveTo;
            }

            set
            {
                m_MoveTo = value;
            }
        }
    }
}
