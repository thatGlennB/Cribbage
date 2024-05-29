using Cribbage.Interfaces;

namespace Cribbage.HelperTypes
{
    internal record DrawCardEventArgs(ICard? PrecedingDraw, ICard? NextDraw);
}
