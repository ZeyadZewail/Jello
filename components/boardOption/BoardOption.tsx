import Board from "@/types/Board";
import { useRouter } from "next/navigation";

const BoardOption = ({ board }: { board: Board }) => {
	const dateCreated = new Date(board.created);
	const { push } = useRouter();

	return (
		<div
			className="items-center flex justify-center cursor-pointer"
			onClick={() => {
				push(`/board/${board.id}`);
			}}>
			<div
				style={{ minWidth: "14rem" }}
				className="py-6 px-4 w-56 h-56 rounded-xl flex flex-col gap-4 bg-primary bg-opacity-10 hover:bg-opacity-5">
				<h1 className="text-lg text-center font-semibold">{board.name}</h1>
				<div className="flex-grow text-sm">{board.description}</div>
				<p className="text-sm text-primary self-end">{`${dateCreated.toLocaleDateString("en-gb")}`}</p>
			</div>
		</div>
	);
};

export default BoardOption;
