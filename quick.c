#include<stdlib.h>
#include<stdio.h>
#include<time.h>

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
  int size = 500000;
  int arr[size];
  for (int i = 0; i < size; i++)
        arr[i] = rand() % 10000;
  
  // for(int i=0; i<8;i++) printf("%d ", abc[i]);
  
  clock_t begin = clock();
  quickSort(arr, 0, size);
  clock_t end = clock();
  double time_spent = (double)(end - begin) / CLOCKS_PER_SEC;

  printf("Time spent: %lf\n", time_spent);

  // for(int i=0; i<8;i++) printf("%d ", abc[i]);

}