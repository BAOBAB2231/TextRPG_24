using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Interface
{
    public interface IScene
    {
        // 장면을 업데이트하고 다음 장면을 반환하거나, 게임 종료 시 null을 반환합니다.
        IScene Update();
    }
}
