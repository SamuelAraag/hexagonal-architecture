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

        [Fact]
        public void TesteComAlgoritmoDeOrdenacaoQuicksortListaVazia()
        {
            var numeros = new List<int> { };

            var ordenador = new Ordenador();

            var listaOrdenada = ordenador.Quicksort(numeros);

            Assert.Empty(listaOrdenada!);
        }

        [Fact]
        public void ApenasUmNumero()
        {
            var numeros = new List<int> { 10 };

            var ordenador = new Ordenador();

            var listaOrdenada = ordenador.Quicksort(numeros);

            var listaEsperada = new List<int> { 10 };
            Assert.Equal(listaEsperada, listaOrdenada);
        }

        [Fact]
        public void ApenasDoisNumerosJaOrdenados()
        {
            var numeros = new List<int> { 10, 20 };

            var ordenador = new Ordenador();

            var listaOrdenada = ordenador.Quicksort(numeros);

            var listaEsperada = new List<int> { 10, 20 };
            Assert.Equal(listaEsperada, listaOrdenada);
        }

        [Fact]
        public void ApenasDoisNumerosDesordenados()
        {
            var numeros = new List<int> { 20, 10 };

            var ordenador = new Ordenador();

            var listaOrdenada = ordenador.Quicksort(numeros);

            var listaEsperada = new List<int> { 10, 20 };
            Assert.Equal(listaEsperada, listaOrdenada);
        }

        [Fact]
        public void TresNumerosDesordenados()
        {
            var numeros = new List<int> { 20, 10, 30 };

            var ordenador = new Ordenador();

            var listaOrdenada = ordenador.Quicksort(numeros);

            var listaEsperada = new List<int> { 10, 20, 30 };
            Assert.Equal(listaEsperada, listaOrdenada);
        }

        [Fact]
        public void MuitosNumerosDesordenados()
        {
            var numeros = new List<int> { 20, 10, 30, 1, 80, 100, 45, 2 };

            var ordenador = new Ordenador();

            var listaOrdenada = ordenador.Quicksort(numeros);

            var listaEsperada = new List<int> { 1, 2, 10, 20, 30, 45, 80, 100 };
            Assert.Equal(listaEsperada, listaOrdenada);
        }

        [Theory]
        [MemberData(nameof(ListasDeNumeros))]
        public void NumerosAleatorios(List<int> numeros)
        {
            var ordenador = new Ordenador();

            var ordenados = ordenador.Quicksort(numeros);

            numeros.Sort();
            Assert.Equal(numeros, ordenados);
        }

        public class Ordenador()
        {
            public List<int> Quicksort(List<int> numeros)
            {
                //casos que não precisa de ordençao
                var ehDesnecessarioOrdenar = numeros.Count < 2;

                if (ehDesnecessarioOrdenar) return numeros;

                if (numeros?.Count == 2) //quando somente dois valores
                {
                    if (numeros[0] < numeros[1]) //se já estiverem ordenados
                    {
                        return numeros;
                    }
                    else //quando precisar inverter o valor
                    {
                        var firstValue = numeros.First();
                        numeros[0] = numeros[1];
                        numeros[1] = firstValue;
                    }

                    return numeros;
                }

                //qual é o caso base?
                //quando existe dois numeros, chamo o mesmo ordenador

                //particiona o array
                var pivo = numeros.First();
                var menores = new List<int>();
                var maiores = new List<int>();

                for (int i = 1; i < numeros.Count; i++) //i = 1 e ignora o primeiro item = que é o pivo
                {
                    if (numeros[i] < pivo)
                    {
                        menores.Add(numeros[i]);
                    }
                    else
                    {
                        maiores.Add(numeros[i]);
                    }
                }

                var numerosOrdenados = Quicksort(menores);
                numerosOrdenados.Add(pivo);
                numerosOrdenados.AddRange(Quicksort(maiores));

                return numerosOrdenados;
            }

        }
        public static IEnumerable<object[]> ListasDeNumeros =>
            new List<object[]>
            {
                new object[] { new List<int> { 1, 2, 3 } },
                new object[] { new List<int> { 5, 4, 3, 2, 1 } },
                new object[] { new List<int> { 10, 1, 7 } },
                new object[] { new List<int> { 9, 4, 7, 2, 8, 1, 5, 3, 6 } },
                new object[] { new List<int> { 1000, 999, 500, 1, 250, 750, 100, 50, 900, 0 } },
            };
    }
}
