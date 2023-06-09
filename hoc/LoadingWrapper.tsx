import Spinner from "@/components/spinner/Spinner";
import { FC, PropsWithChildren } from "react";

interface LoadingInterface {
	loading: boolean;
	spinnerSize: number;
}

const LoadingWrapper: FC<PropsWithChildren<LoadingInterface>> = ({ loading, spinnerSize, children }) => {
	return loading ? <Spinner size={spinnerSize} /> : <>{children}</>;
};

export default LoadingWrapper;
