using Domain.Entities;
using Domain.Enums;

namespace DomainTests.Bookings
{
    public class BookingTests
    {
        [Fact]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.Equal(StatusBooking.Created, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(ActionState.Pay);

            Assert.Equal(StatusBooking.Paid, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(ActionState.Cancel);

            Assert.Equal(StatusBooking.Canceled, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToFinishedWhenFinishingABookingWithPaidStatus()
        {
            var booking = new Booking();
            booking.ChangeState(ActionState.Pay);

            booking.ChangeState(ActionState.Finish);

            Assert.Equal(StatusBooking.Finished, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToRefoundedWhenRefoundingABookingWithPaidStatus()
        {
            var booking = new Booking();
            booking.ChangeState(ActionState.Pay);

            booking.ChangeState(ActionState.Refound);

            Assert.Equal(StatusBooking.Refounded, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusBackToCreatedWhenReopeningACanceledBooking()
        {
            var booking = new Booking();
            booking.ChangeState(ActionState.Cancel);

            booking.ChangeState(ActionState.Reopen);

            Assert.Equal(StatusBooking.Created, booking.CurrentStatus);
        }

        // ---- Casos inválidos ----

        [Fact]
        public void ShouldNotChangeStatusWhenTryingToPayAPaidBooking()
        {
            var booking = new Booking();
            booking.ChangeState(ActionState.Pay);

            booking.ChangeState(ActionState.Pay);

            Assert.Equal(StatusBooking.Paid, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenTryingToCancelAPaidBooking()
        {
            var booking = new Booking();
            booking.ChangeState(ActionState.Pay);

            booking.ChangeState(ActionState.Cancel);

            Assert.Equal(StatusBooking.Paid, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenTryingToFinishACanceledBooking()
        {
            var booking = new Booking();
            booking.ChangeState(ActionState.Cancel);

            booking.ChangeState(ActionState.Finish);

            Assert.Equal(StatusBooking.Canceled, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenTryingToRefoundACreatedBooking()
        {
            var booking = new Booking();

            booking.ChangeState(ActionState.Refound);

            Assert.Equal(StatusBooking.Created, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenTryingToReopenAFinishedBooking()
        {
            var booking = new Booking();
            booking.ChangeState(ActionState.Pay);
            booking.ChangeState(ActionState.Finish);

            booking.ChangeState(ActionState.Reopen);

            Assert.Equal(StatusBooking.Finished, booking.CurrentStatus);
        }

        //[Fact]
        //public void TesteComAlgoritmoDeOrdenacaoQuicksortListaVazia()
        //{
        //    var numeros = new List<int> { };

        //    var ordenador = new Ordenador();

        //    var listaOrdenada = ordenador.Quicksort(numeros);

        //    Assert.Empty(listaOrdenada!);
        //}

        //[Fact]
        //public void ApenasUmNumero()
        //{
        //    var numeros = new List<int> { 10 };

        //    var ordenador = new Ordenador();

        //    var listaOrdenada = ordenador.Quicksort(numeros);

        //    var listaEsperada = new List<int> { 10 };
        //    Assert.Equal(listaEsperada, listaOrdenada);
        //}

        //[Fact]
        //public void ApenasDoisNumerosJaOrdenados()
        //{
        //    var numeros = new List<int> { 10, 20 };

        //    var ordenador = new Ordenador();

        //    var listaOrdenada = ordenador.Quicksort(numeros);

        //    var listaEsperada = new List<int> { 10, 20 };
        //    Assert.Equal(listaEsperada, listaOrdenada);
        //}

        //[Fact]
        //public void ApenasDoisNumerosDesordenados()
        //{
        //    var numeros = new List<int> { 20, 10 };

        //    var ordenador = new Ordenador();

        //    var listaOrdenada = ordenador.Quicksort(numeros);

        //    var listaEsperada = new List<int> { 10, 20 };
        //    Assert.Equal(listaEsperada, listaOrdenada);
        //}

        //[Fact]
        //public void TresNumerosDesordenados()
        //{
        //    var numeros = new List<int> { 20, 10, 30 };

        //    var ordenador = new Ordenador();

        //    var listaOrdenada = ordenador.Quicksort(numeros);

        //    var listaEsperada = new List<int> { 10, 20, 30 };
        //    Assert.Equal(listaEsperada, listaOrdenada);
        //}

        //public class Ordenador()
        //{
        //    public List<int> Quicksort(List<int> numeros)
        //    {
        //        //casos que não precisa de ordençao
        //        var ehVazio = numeros.Count == 0;
        //        var ehSomenteUmValor = numeros.Count == 1;

        //        if (ehVazio || ehSomenteUmValor) return numeros;

        //        //qual é o caso base?
        //        //quando existe dois numeros, chamo o mesmo ordenador

        //        if(numeros?.Count == 2) //quando somente dois valores
        //        {
        //            if (numeros[0] < numeros[1]) //se já estiverem ordenados
        //            {
        //                return numeros;
        //            }
        //            else //quando precisar inverter o valor
        //            {
        //                var firstValue = numeros.First();
        //                numeros[0] = numeros[1];
        //                numeros[1] = firstValue;
        //            }
        //        }
        //        else
        //        {
        //            //esse é o caso do particionamento
        //                //pivo com duas listas, uma antes e uma depois;

        //            //quando mais de dois - preciso da recursividade
        //            //preciso do caso do pivo
        //            //pega o primeiro como pivo
        //            var pivo = numeros!.First(); //o valor 20 esta indo pro final
        //            numeros?.RemoveAt(0);

        //            //antes de chamar -r, pegue os valores menores
        //            var listaOrdenada = Quicksort(numeros!);
        //            listaOrdenada.Add(pivo);
        //            return listaOrdenada;
        //        }

        //        return numeros;
        //    }
        //}
    }
}
