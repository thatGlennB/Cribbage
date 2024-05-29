using Cribbage.Model;

namespace Cribbage.HelperTypes
{
    internal record DrawCardEventArgs(Card? PrecedingDraw, Card? NextDraw);
}
