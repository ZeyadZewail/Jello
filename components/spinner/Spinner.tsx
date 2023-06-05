import theme from "../../constants/theme";

const Spinner = ({ size }: { size: number }) => (
	<div
		style={{
			width: `${size}px`,
			height: `${size}px`,
			borderRadius: "50%",
			border: `${size / 8}px solid ${theme["primary"]}`,
			borderTop: `${size / 8}px solid ${theme["primary-button"]}`,
		}}
		className="bg-transparent animate-spin"
	/>
);

export default Spinner;
