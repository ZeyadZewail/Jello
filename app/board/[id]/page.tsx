"use client";

import LoadingWrapper from "@/hoc/LoadingWrapper";
import Board from "@/types/Board";
import SignalCommand from "@/types/SignalCommand";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

const BoardPage = ({ params }: { params: { id: string } }) => {
	const [connection, setConnection] = useState<HubConnection>();
	const [board, setBoard] = useState<Board>();
	const [loading, setLoading] = useState(true);
	const { push } = useRouter();

	const [tempString, setTempString] = useState("");

	useEffect(() => {
		const getBoards = async () => {
			const response = await fetch(`https://localhost:7174/api?id=${params.id}`, { method: "GET" });
			const responseData = await response.json();
			if (!response.ok) {
				alert("NOT OK");
				return;
			}

			setBoard(responseData);

			const newConnection = new HubConnectionBuilder()
				.withUrl("https://localhost:7174/hubs/BoardControlHub")
				.withAutomaticReconnect()
				.build();

			setConnection(newConnection);

			setLoading(false);
		};

		getBoards();

		return () => {
			if (connection) {
				connection.stop();
			}
		};
	}, [params.id]);

	useEffect(() => {
		if (connection) {
			connection
				.start()
				.then((result) => {
					connection.on(params.id, (signalCommand: SignalCommand) => {
						commands[signalCommand.commandName](signalCommand.payload);
						console.log(signalCommand);
					});
				})
				.catch((e) => console.log("Connection failed: ", e));
		}
	}, [connection]);

	const renameBoardBroadcast = async () => {
		if (connection) {
			if (tempString !== "") {
				try {
					await connection.send("RenameBoard", params.id, tempString);
				} catch (e) {
					console.log(e);
				}
			}
		} else {
			alert("No connection to server yet.");
		}
	};

	const commands: { [key: string]: Function } = {
		renameBoard: (payload: any) => {
			setBoard({ ...board, name: payload } as Board);
		},
	};

	return (
		<LoadingWrapper loading={loading} spinnerSize={80}>
			<div>boardpage {board?.name}</div>
			<div className="flex gap-2">
				<input
					className="text-secondary"
					value={tempString}
					onChange={(e) => {
						setTempString(e.target.value);
					}}
				/>
				<button onClick={renameBoardBroadcast} className="bg-primary-button ">
					Rename
				</button>
			</div>
		</LoadingWrapper>
	);
};

export default BoardPage;
