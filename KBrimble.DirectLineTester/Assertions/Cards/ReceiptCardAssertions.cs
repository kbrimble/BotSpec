using System;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface ICanAssertFacts
    {
        IFactAssertions WithFactThat();
    }

    public interface ICanAssertReceiptItems
    {
        IReceiptItemAssertions WithReceiptItemThat();
    }

    public interface IReceiptCardAssertions : ICanAssertButtons, ICanAssertTapActions, ICanAssertCardImages, ICanAssertReceiptItems, ICanAssertFacts
    {
        
    }

    class ReceiptCardAssertions : IReceiptCardAssertions
    {
        private readonly ReceiptCard _receiptCard;

        public ReceiptCardAssertions(ReceiptCard receiptCard)
        {
            _receiptCard = receiptCard;
        }
        public ICardActionAssertions WithButtonsThat()
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions WithTapActionThat()
        {
            throw new NotImplementedException();
        }

        public ICardImageAssertions WithCardImageThat()
        {
            throw new NotImplementedException();
        }

        public IReceiptItemAssertions WithReceiptItemThat()
        {
            throw new NotImplementedException();
        }

        public IFactAssertions WithFactThat()
        {
            throw new NotImplementedException();
        }
    }
}
