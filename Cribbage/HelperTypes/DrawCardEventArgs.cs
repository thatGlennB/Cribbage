using Cribbage.Interfaces;

namespace Cribbage.HelperTypes
{
    internal record DrawCardEventArgs(Card? PrecedingDraw, Card? NextDraw);
}
