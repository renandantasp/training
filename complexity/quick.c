#include<stdlib.h>
#include<stdio.h>
#include<time.h>

#define SIZE 100000

int* create_array(int size){
  return (int *) malloc(size * sizeof(int));
}

int partition(int arr[], int ini, int end){
  int pivot = arr[end];
  int i = ini - 1;

  for (int j = ini; j<end; j++){
    if(arr[j] < pivot){
      int tmp = arr[++i];
      arr[i] = arr[j];
      arr[j] = tmp;
    }
  }

  int tmp = arr[++i];
  arr[i] = arr[end];
  arr[end] = tmp;
  return i;

}


void quickSort(int arr[], int ini, int end){
  if (ini >= end) return;

  int pi = partition(arr, ini, end);

  quickSort(arr,ini,pi-1);
  quickSort(arr,pi+1,end);

}


int main(){
  int* arr = create_array(SIZE);
  for (int i = 0; i < SIZE; i++)
        arr[i] = rand() % RAND_MAX;
  
  // for(int i=0; i<SIZE;i++) printf("%d ", arr[i]);
  
  clock_t begin = clock();
  quickSort(arr, 0, SIZE);
  clock_t end = clock();
  double time_spent = (double)(end - begin) / CLOCKS_PER_SEC;

  printf("Time spent: %lf\n", time_spent);

  // for(int i=0; i<SIZE;i++) printf("%d ", arr[i]);

}