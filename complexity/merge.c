#include<stdlib.h>
#include<stdio.h>
#include<time.h>

#define SIZE 10000000

int* create_array(int size){
  return (int *) malloc(size * sizeof(int));
}

void merge(int arr[], int ini, int mid, int end){
  int i, j, k;

  int s1 = mid - ini + 1;
  int s2 = end - mid;

  int *aux1 = create_array(s1);
  int *aux2 = create_array(s2);
  
  for(i = 0; i < s1; i++) aux1[i] = arr[ini + i];
  for(i = 0; i < s2; i++) aux2[i] = arr[mid + i + 1];

  i = j = 0;
  k = ini;

  while( i < s1 && j < s2)
    if(aux1[i] <= aux2[j]) 
      arr[k++] = aux1[i++];
    else 
      arr[k++] = aux2[j++];
    

  while(i < s1)
    arr[k++] = aux1[i++];
  

  while(j < s2)
    arr[k++] = aux2[j++];

  // free(aux1);
  // free(aux2);

}


void mergeSort(int arr[], int ini, int end){
  if (ini >= end) return;

  int mid = (ini + end)/2;
  
  mergeSort(arr, ini, mid);
  mergeSort(arr, mid+1, end);
  
  merge(arr, ini, mid, end);
  
}


int main(){
  int* arr = create_array(SIZE);
  for (int i = 0; i < SIZE; i++)
        arr[i] = rand() % RAND_MAX;
  
  // for(int i=0; i<SIZE;i++) printf("%d ", arr[i]);
  
  clock_t begin = clock();
  mergeSort(arr, 0, SIZE);
  clock_t end = clock();
  double time_spent = (double)(end - begin) / CLOCKS_PER_SEC;

  printf("Time spent: %lf\n", time_spent);

  // for(int i=0; i<SIZE;i++) printf("%d ", arr[i]);

}