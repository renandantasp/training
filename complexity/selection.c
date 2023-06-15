#include<stdlib.h>
#include<stdio.h>
#include <time.h>

#define SIZE 50000

void selectionSort(int arr[], int n) {
    for (int i = 0; i < n - 1; i++) {
        int min_idx = i;
        for (int j = i + 1; j < n; j++) {
            if (arr[j] < arr[min_idx]) {
                min_idx = j;
            }
        }
        int temp = arr[i];
        arr[i] = arr[min_idx];
        arr[min_idx] = temp;
    }
}

int main(){
  int arr[SIZE];
  for (int i = 0; i < SIZE; i++)
        arr[i] = rand() % 10000;
  
  // for(int i=0; i<SIZE;i++) printf("%d ", abc[i]);
  
  clock_t begin = clock();
  selectionSort(arr, SIZE);
  clock_t end = clock();
  double time_spent = (double)(end - begin) / CLOCKS_PER_SEC;

  printf("Time spent: %lf\n", time_spent);

  // for(int i=0; i<SIZE;i++) printf("%d ", abc[i]);

}


// int main() {
//   srand(time(NULL));
//   int steps;
//   int size = 50;
//   int arr[50];
//   int asc[50];
//   int des[50];
//   int max = 500;
//   int val = 0;

//   for (int i = 0; i < size; i++)
//         arr[i] = rand() % max;

//   for (int i = 0; i < size; i++)
//         asc[i] = val++;

//   for (int i = 0; i < size; i++)
//         des[i] = --val;
  
//   ///////////////////////////////////////////////////////////////////

//   // printf("random: ");
//   // for (int i = 0; i < size; i++)
//   //   printf("%d ", arr[i]);

//   steps = selectionSort(arr, size);
//   printf("\nrandom: ");

//   // for (int i = 0; i < size; i++)
//   //   printf("%d ", arr[i]);

//   printf("\nsteps: %d", steps);
//   printf("\n\n");
  
//   ///////////////////////////////////////////////////////////////

//   // printf("asc: ");
//   // for (int i = 0; i < size; i++)
//   //   printf("%d ", asc[i]);

//   steps = selectionSort(arr, size);
//   printf("\nasc: ");

//   // for (int i = 0; i < size; i++)
//   //   printf("%d ", asc[i]);
  
//   printf("\nsteps: %d", steps);
//   printf("\n\n");
//   /////////////////////////////////////////////////////////////////

//   // printf("des: ");
//   // for (int i = 0; i < size; i++)
//   //   printf("%d ", des[i]);

//   steps = selectionSort(des, size);
//   printf("\ndes: ");

//   // for (int i = 0; i < size; i++)
//   //   printf("%d ", des[i]);

//   printf("\nsteps: %d", steps);


//   return 0;
// }