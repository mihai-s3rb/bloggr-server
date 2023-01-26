type TError = {
  message: string;
  errors?: TError[]; 
}

export default TError;